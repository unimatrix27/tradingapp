import { RefDataService } from './../../services/refdata.service';
import { InstrumentNameValidator } from './../validators/instrument-name-validator';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Http} from '@angular/http';
import {Observable} from "rxjs/Observable";
import 'rxjs/add/operator/filter';// load this in main.ts
import 'rxjs/add/operator/debounceTime';// load this in main.ts
import 'rxjs/add/operator/switchMap';// load this in main.ts


@Component({
  selector: 'app-brokerinstruments-form',
  templateUrl: './brokerinstruments-form.component.html',
  styleUrls: ['./brokerinstruments-form.component.css']
})


export class BrokerInstrumentsFormComponent implements OnInit {

  rForm: FormGroup;
  post:any;                     // A property for our submitted form
  name:string = '';
  instrumentName;
  constructor(private http: Http, private refDataService: RefDataService,private fb: FormBuilder) { 

    this.rForm = fb.group({
      'name' : ["", Validators.compose([Validators.required, Validators.minLength(2), Validators.maxLength(50)]),
                                         InstrumentNameValidator.shouldBeUnique],
    });
    
  }

  ngOnInit() {
    this.rForm.controls['name'].valueChanges
    .filter(val => val.length >= 2)
    .debounceTime(500)
    .switchMap(val => this.refDataService.findInstrumentName(val))
    .subscribe(valid => console.log(valid), error => console.log("ERROR: "+error));
  }

  addName(post) {
    this.refDataService.createInstrumentName(post).subscribe(instrumentName => {
      this.instrumentName = instrumentName;
    console.log (this.instrumentName);
    });
  }
}

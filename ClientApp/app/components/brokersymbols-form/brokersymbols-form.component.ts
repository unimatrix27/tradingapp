import { RefDataService } from './../../services/refdata.service';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import {CheckboxModule} from 'primeng/primeng';
import {AutoCompleteModule} from 'primeng/primeng';



@Component({
  selector: 'app-brokersymbols-form',
  templateUrl: './brokersymbols-form.component.html',
  styleUrls: ['./brokersymbols-form.component.css']
})

export class BrokerSymbolsFormComponent implements OnInit {
  selectedInstrumentName;
  instrumentNames;
  brokers;
  selectedBroker;
  exchanges;
  selectedExchange;
  results: string[] = ['val1','val2','val1','val2'];
  selectedValues: string[] = ['val1','val2'];
  constructor(private refDataService: RefDataService) { }
  tests = ["x","z","w"];

  ngOnInit() {
    this.refDataService.nextRefImage().subscribe(instrumentNames => {
      this.instrumentNames = instrumentNames;
      console.log("InstrumentNames",this.instrumentNames);
    });
    this.refDataService.getBrokers().subscribe(brokers => {
      this.brokers = brokers;
      console.log("Brokers",this.brokers);
    });
    this.refDataService.getExchanges().subscribe(exchanges => {
      this.exchanges = exchanges;
      console.log("Exchanges",this.exchanges);
    });
  }

  onClickTest(){
    console.log("Click");
  }

  valueChanged(newVal) {
    console.log("value is changed to ", newVal);
  }

  search(event){
    console.log(event);
  }

}

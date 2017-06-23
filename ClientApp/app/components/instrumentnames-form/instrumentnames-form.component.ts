import { Component, OnInit } from '@angular/core';
import { RefDataService } from './../../services/refdata.service';
import { FormBuilder, FormGroup, Validators} from '@angular/forms';

@Component({
  selector: 'app-instrumentnames-form',
  templateUrl: './instrumentnames-form.component.html',
  styleUrls: ['./instrumentnames-form.component.css']
})


export class InstrumentNamesFormComponent implements OnInit {
  rForm: FormGroup;
  post:any;
  description:string = "";
  name: string = "";
  
  
  
  selectedInstrumentName;
  instrumentNames;
  brokers;
  selectedBroker;
  exchanges;
  selectedExchange;
  brokerInstruments;

  filteredExchanges: any[];

  constructor(private fb: FormBuilder,private refDataService: RefDataService) { }

  ngOnInit() {
    this.refDataService.getInstrumentNames().subscribe(instrumentNames => {
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
    this.refDataService.getBrokerInstruments().subscribe(brokerInstruments => {
      this.brokerInstruments = brokerInstruments;
      console.log("Broker Instruments",this.brokerInstruments);
    });
  }

  filterExchanges(event) {
             console.log(event);
     this.filteredExchanges = [];
     for(let i = 0; i < this.exchanges.length; i++) {
        let exchange = this.exchanges[i];
        if(exchange.name.toLowerCase().indexOf(event.query.toLowerCase()) >= 0) {
          this.filteredExchanges.push(exchange);
        }
     }
  }
    
  handleDropdownClick() {
        this.filteredExchanges = [];
        
        //mimic remote call
        setTimeout(() => {
            this.filteredExchanges = this.exchanges;
        }, 100)
  }

    handleSelect(event) {
        console.log(event);
        this.selectedExchange=event.name;
        
    }

}

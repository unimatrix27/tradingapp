import { Injectable } from '@angular/core';
import { Http, Headers,RequestOptions } from '@angular/http';
import 'rxjs/add/operator/map';

@Injectable()
export class RefDataService {


  constructor(private http: Http) { 
        
  }

  getRefImages() {
      return this.http.get('/api/refimages/')
          .map(res => res.json());
  }

  nextRefImage(){
    return this.http.get('/api/refimages/next')
    .map(res => res.json());
  }

  createInstrumentName(instrumentName){
    let headers = new Headers({ 'Content-Type': 'application/json' });
    let options = new RequestOptions({ headers: headers }); 
    return this.http.post('/api/instrumentnames/',JSON.stringify(instrumentName),options)
    .map(res => res.json());
  }

  assignRefImage(refImageId,brokerInstrumentId) {
      let headers = new Headers({ 'Content-Type': 'application/json' });
      let options = new RequestOptions({ headers: headers });
      return this.http.put('/api/refimages/'+refImageId, brokerInstrumentId, options)
          .map(res => res.json());
  }

  findInstrumentName(name: string){
    return this.http.get('/api/instrumentnames/find/'+name)
    .map(res => res.json());
  }

  getInstrumentNames(){
    return this.http.get('/api/instrumentnames')
    .map(res => res.json());
  }
  
  getExchanges(){
    return this.http.get('/api/exchanges')
    .map(res => res.json());
  }

    getBrokers(){
    return this.http.get('/api/brokers')
    .map(res => res.json());
  }
  getBrokerInstruments(){
    return this.http.get('/api/brokerinstruments')
    .map(res => res.json());
  }

  getBrokerInstrumentsScreener(id:number) {
      return this.http.get('/api/brokerinstruments/screener/'+id)
          .map(res => res.json());
  }
   getBrokerInstrumentsAll(){
    return this.http.get('/api/instrumentnamesall')
    .map(res => res.json());
  }
}

import { Screener } from './../../swag/model';

import { Component } from '@angular/core';
import { Client, SignalState, SignalResource,TradeResource,SignalStepResource,TradeStepResource,OrderResource,TradeStepResult,SignalType,SignalStepType,TradeDirection,TradeStepType, OrderFunction, OrderType, OrderState} from '../../swag/model';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/switchMap';
import {Observable} from 'rxjs/Rx';

@Component({
    selector: 'fetchdata',
    templateUrl: './fetchdata.component.html',
    styleUrls: ['./fetchdata.component.css']
})

export class FetchDataComponent {
    public trades: TradeResource[];
    public signals: SignalResource[];
    public sst: typeof SignalStepType;
    public st: typeof SignalType;
    public td: typeof TradeDirection;
    public tst: typeof TradeStepType;
    public of: typeof OrderFunction;
    public ot: typeof OrderType;
    public os: typeof OrderState;
    public tsr: typeof TradeStepResult;
    public ss: typeof SignalState;
    public foundTrades: boolean;
    public foundSignals: boolean;
    public executedLevel: number;
    public instrumentName: string;
    public tempTradeId: number;
    public inputFocused;
    displayTradeFilled: boolean = false;
    public gesamtPnL: number;
    public screeners: Screener[];
    public dax;
    public mdax;
    public dow;

    constructor(private client: Client) {
        this.sst = SignalStepType;
        this.st = SignalType;
        this.td = TradeDirection;
        this.tst = TradeStepType;
        this.of = OrderFunction;
        this.ot = OrderType;
        this.os = OrderState;
        this.tsr = TradeStepResult;
        this.foundTrades = false;
        this.foundSignals = false;
        this.executedLevel = 0;
        this.gesamtPnL = 0;
        
        
    }

    ngOnInit() {
        this.loadSignals();
        this.loadTrades();
        Observable.timer(0, 600000) // Run every 60 seconds
            .switchMap(() => this.client.screeners_lastScreeners())
            .catch(error => Observable.of(error))

            .subscribe(data => {
            this.screeners =  data;
            this.dax = this.screeners.find(x => x.screenerTypeId==2).timeStamp.calendar();
            this.mdax = this.screeners.find(x => x.screenerTypeId==7).timeStamp.calendar();
            this.dow = this.screeners.find(x => x.screenerTypeId==8).timeStamp.calendar();
     })
    }

    public loadSignals(){
            this.client.signal_GetOpen().subscribe(result => {
            this.signals = result;
            if(this.signals.length > 0){
                this.foundSignals = true;
                this.signals.forEach(element => {element.signalSteps.sort(this.sortCreatedSST);});
                console.log(this.signals);
            }else{
                console.log("No Signals returned");
            }
        });
    }

    public sortCreatedTST(a: TradeStepResource, b: TradeStepResource){
        if(a.created > b.created) return 1;
        return -1;
    }

    public sortCreatedSST(a: SignalStepResource, b: SignalStepResource){
        if(a.created > b.created) return 1;
        return -1;
    }

    public loadTrades(){
        this.client.trade_GetTrades().subscribe(result => {
            this.trades = result;
            if(this.trades.length > 0){
                this.foundTrades = true;
                this.gesamtPnL=0;
                this.trades.forEach(element => {
                    element.tradeSteps.sort(this.sortCreatedTST);
                    element.signal.signalSteps.sort(this.sortCreatedSST);
                    this.gesamtPnL += element.pnL;
                });
                console.log(this.trades);
            }else{
                console.log("No Trades returned");
            }
        });
    }

    public tradeCancel(trade: TradeResource){
        console.log(trade);
        this.client.trade_CancelTrade(trade.id).subscribe(result => {
            console.log(result);
            this.loadTrades();
            this.loadSignals();
        });
        
    }

    public highlightRow(rowData: TradeResource, rowIndex: number) {
        //return rowData.LeadTimeRemaining + '-hightliting';
        return 'tradebg-' + TradeStepType[rowData.type];

    }

     public tradeChange(trade: TradeResource){
        console.log(trade);
        this.client.trade_UpdateTradeFromSignal(trade.id).subscribe(result => {
            console.log(result);
            this.loadTrades();
        });
        
    }

    public tradeUndo(trade: TradeResource){
        this.client.trade_UndoLastTradeStep(trade.id).subscribe(result => {
            console.log(result);
            this.loadTrades();
        });
    }

    public tradeHide(trade: TradeResource){
        this.client.trade_Hide(trade.id).subscribe(result => {
            console.log(result);
            this.loadTrades();
            this.loadSignals();
        });
    }

    public signalDiscard(signal: SignalResource){
        this.client.trade_DiscardSignal(signal.id).subscribe(result => {
            console.log(result);
            this.loadSignals();
        });
    }

    public tradeConfirm(trade: TradeResource){
        this.client.trade_ConfirmPlaceTrade(trade.id).subscribe(result => {
            console.log(result);
            this.loadTrades();
            this.loadSignals();
        });
    }

    public signalTrade(signal: SignalResource){
        this.client.trade_CreateTrade(signal.id).subscribe(result => {
            console.log(result);
            this.loadTrades();
            this.loadSignals();
        });
    }

    public tradeFilled(trade: TradeResource){
        this.executedLevel = trade.signal.instrument.lastPriceEntry.close;
        this.instrumentName = trade.signal.instrument.name;
        this.tempTradeId = trade.id;
        this.displayTradeFilled = true;
        this.inputFocused = true;
    }

    public tradeFilled2(trade: TradeResource){
        this.displayTradeFilled = false;
        this.client.trade_FilledTrade(this.tempTradeId, this.executedLevel).subscribe(result => {
            console.log(result);
            this.loadTrades();
        });
    }

    public getColor(x, data:TradeResource) {
        var color: string;
        var percent = 100 * data.percentPnL;
        if (data.pnL > 0 ) {
          color = 'linear-gradient(to right, #1aff1a '+percent+'%, transparent 10%)';
        } else if (data.pnL < 0) {
             color = 'linear-gradient(to left, #ffc2b3 '+percent+'%, transparent 10%)';
        } 
        x.parentNode.parentNode.style.background = color; // x.parentNode.parentNode accesses the <td> element of table :)
  }

      public getFgColor(x, data:TradeResource) {
        var color: string;
        if (data.signal.tradeDirection == TradeDirection.Long) {
          color = 'green';
        } else  {
             color = 'red';
        } 
        x.parentNode.parentNode.style.color = color; // x.parentNode.parentNode accesses the <td> element of table :)
  }
}


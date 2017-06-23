import { NgModule } from '@angular/core';
import { FormsModule,ReactiveFormsModule} from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { APP_BASE_HREF, CommonModule } from '@angular/common';

import { ClientModule} from './services.module';

import {DataTableModule,SharedModule,TooltipModule,DialogModule,InputMaskModule} from 'primeng/primeng';
import {ButtonModule} from 'primeng/primeng';
import {CheckboxModule} from 'primeng/primeng';
import {AutoCompleteModule} from 'primeng/primeng';



import { AppComponent } from './components/app/app.component'
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { CounterComponent } from './components/counter/counter.component';

import { RefDataService } from './services/refdata.service';

import { InstrumentNamesFormComponent } from './components/instrumentnames-form/instrumentnames-form.component';
import { BrokerSymbolsFormComponent } from './components/brokersymbols-form/brokersymbols-form.component';
import { BrokerInstrumentsFormComponent } from './components/brokerinstruments-form/brokerinstruments-form.component';
import { RefImageMatcherComponent } from './components/ref-image-matcher/ref-image-matcher.component';
import { RefImageListComponent } from './components/ref-image-list/ref-image-list.component';
import { TradeListComponent } from './components/trade-list/trade-list.component';



@NgModule({
    bootstrap: [ AppComponent ],
    declarations: [
        AppComponent,
        NavMenuComponent,
        CounterComponent,
        FetchDataComponent,
        HomeComponent,
        InstrumentNamesFormComponent,
        BrokerSymbolsFormComponent,
        BrokerInstrumentsFormComponent,
        RefImageMatcherComponent,
        RefImageListComponent,
        TradeListComponent,
    ],
    imports: [
        ClientModule,
        FormsModule,
        ReactiveFormsModule,
        BrowserModule.withServerTransition({ 
            appId: 'tradingapp' // <-- 
         }),
         HttpModule,
        CheckboxModule,
        BrowserAnimationsModule,
        AutoCompleteModule,
        DataTableModule,
        TooltipModule,
         InputMaskModule,
        DialogModule,
        ButtonModule,
        SharedModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'instrumentnames',component: InstrumentNamesFormComponent},
            { path: 'brokersymbols',component: BrokerSymbolsFormComponent},
            { path: 'brokerinstruments',component: BrokerInstrumentsFormComponent},
            { path: 'refimagematcher', component: RefImageMatcherComponent },
            { path: 'refimages', component: RefImageListComponent },
            { path: 'home', component: HomeComponent },
            { path: 'counter', component: CounterComponent },
            { path: 'fetch-data', component: FetchDataComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ],
    providers: [
       RefDataService
    ]
})
export class AppModuleShared {
}

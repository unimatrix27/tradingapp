import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';
import { Client } from './swag/model';

@NgModule({
    imports: [
        HttpModule
    ],
    providers: [
        Client
    ]
})
export class ClientModule {
}
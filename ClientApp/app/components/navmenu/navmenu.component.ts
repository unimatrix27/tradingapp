import { Component } from '@angular/core';
import { Client} from '../../swag/model';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/switchMap';
import {Observable} from 'rxjs/Rx';

@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css']
})
export class NavMenuComponent {
    public numMatchRefs: any;
    constructor(private client: Client) {
        
        
    }

    ngOnInit() {
        Observable.timer(0, 10000) // Run every 10 seconds
            .switchMap(() => this.client.refImage_getCount())
      // handle http errors here to prevent
      // breaking of interval observable
      .catch(error => Observable.of(error))
    .subscribe(data => {
      this.numMatchRefs =  data;
      if(data == 0) this.numMatchRefs="";

    })

    }
}

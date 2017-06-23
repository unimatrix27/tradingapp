import { Component, OnInit } from '@angular/core';
import { RefDataService } from './../../services/refdata.service';

@Component({
  selector: 'app-ref-image-list',
  templateUrl: './ref-image-list.component.html',
  styleUrls: ['./ref-image-list.component.css']
})


export class RefImageListComponent implements OnInit {
    allRefImages;
    resultRefImage;

    constructor(private refDataService: RefDataService) { }

    ngOnInit() {
        this.loadData();
    }

    loadData(){
                this.refDataService.getRefImages().subscribe(refImages => {
            this.allRefImages = refImages;
            console.log(this.allRefImages);
        });
    }
    delete(id) {
        this.refDataService.assignRefImage(id,null).subscribe(refImage => {
            this.resultRefImage = refImage;
           console.log(this.resultRefImage)
           this.allRefImages = [];
           this.loadData();
        })


    }
}

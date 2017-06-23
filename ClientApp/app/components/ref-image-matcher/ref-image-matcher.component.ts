import { Component, OnInit } from '@angular/core';
import { RefDataService } from './../../services/refdata.service';
import { FormBuilder, FormGroup, Validators} from '@angular/forms';

@Component({
    selector: 'app-ref-image-matcher',
    templateUrl: './ref-image-matcher.component.html',
    styleUrls: ['./ref-image-matcher.component.css']
})


export class RefImageMatcherComponent implements OnInit {
    rForm: FormGroup;
    post: any;                     // A property for our submitted form
    selectedBrokerInstrumentId = 0;
    filteredBrokerInstruments;
    allBrokerInstruments;
    brokerInstruments;
    refImageId;
    refImage: any;
    unreferencedImages = "none";
    resultRefImage;


    constructor(private refDataService: RefDataService, private fb: FormBuilder) {
        this.rForm = fb.group({
            'brokerInstrument': ["", Validators.required]
        });
    }

    ngOnInit() {
        this.getNextImage();
    }

    getNextImage() {
        this.refDataService.nextRefImage().subscribe(refImage => {
            this.refImage = refImage;
            this.refImageId = refImage.id;
            this.unreferencedImages = refImage.unreferencedImages;
            this.refDataService.getBrokerInstrumentsScreener(refImage.screenerTypeId).subscribe(brokerInstruments => {
                this.brokerInstruments = brokerInstruments;
                console.log(this.brokerInstruments);
            });
            console.log(this.refImage);
        });
    }

    filterBrokerInstruments(event) {
        this.filteredBrokerInstruments = [];
        for (let i = 0; i < this.brokerInstruments.length; i++) {
            let brokerInst = this.brokerInstruments[i];
            if (brokerInst.instrumentNameName.toLowerCase().indexOf(event.query.toLowerCase()) >= 0) {
                this.filteredBrokerInstruments.push(brokerInst);
            }
        }
    }

    handleDropdownClick() {
        this.filteredBrokerInstruments = [];
        setTimeout(() => {
            this.filteredBrokerInstruments = this.brokerInstruments;
        }, 50)
    }

    handleBrokerInstrumentSelect(event) {

        this.selectedBrokerInstrumentId = event.id
    }

    save() {
        this.refDataService.assignRefImage(this.refImageId,this.selectedBrokerInstrumentId).subscribe(refImage => {
            this.resultRefImage = refImage;
            
            this.getNextImage();
        });
        this.rForm.reset();
        this.selectedBrokerInstrumentId=0;
        this.filteredBrokerInstruments = [];
        this.handleDropdownClick();
    }
}

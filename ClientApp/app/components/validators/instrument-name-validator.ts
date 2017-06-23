import { RefDataService } from './../../services/refdata.service';
import { FormControl} from '@angular/forms';


export class InstrumentNameValidator {
  

  static cannotContainSpace(formControl: FormControl){
    if(formControl.value.indexOf(' ')>= 0)
      return {cannotContainSpace: true};
    return null;
  }

  static shouldBeUnique(formControl: FormControl){
      return new Promise (( resolve, reject) => {
        
        
        
        setTimeout(function(){
          if(formControl.value == "test")
            resolve ({shouldBeUnique: true});
          else 
             resolve(null);
        },30);
      });
  }


}

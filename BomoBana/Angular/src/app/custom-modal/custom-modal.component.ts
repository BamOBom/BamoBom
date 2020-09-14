
import { Component } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { isUndefined } from 'util';

@Component({
  selector: 'app-custom-modal',
  templateUrl: './custom-modal.component.html',
  styleUrls: ['./custom-modal.component.less']
})

export class CustomModalComponent {

  title= 'Aceptar';
  bodyHTML;
  okButtonText = 'Aceptar';
  cancelButtonText = 'Cerrar';
  successCallback;
  cancelCallback;

  constructor(public activeModal: NgbActiveModal) {

   
  }

  successFunction() {
    this.activeModal.close('Close click');
    if (!isUndefined(this.successCallback)) {
      this.successCallback();
    }
  }

  cancelFunction() {
    this.activeModal.close('Close click');
    if (!isUndefined(this.cancelCallback)) {
      this.cancelCallback();
    }
  }

}
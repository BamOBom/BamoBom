import { Component, OnInit,Injectable ,EventEmitter, Output } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CustomModalComponent } from '../../custom-modal/custom-modal.component';
// import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AppAuthService } from '../../auth/app-auth.service';
import { ShareServiceService } from '../../share-service.service';
import { User } from '../../model/user';
import { Observable } from 'rxjs';
@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  form;
  userModal = new User();
  @Output() getLoggedInName: EventEmitter<any> = new EventEmitter();
  constructor(private modalService: NgbModal,
    //private fb: FormBuilder,
    private myRoute: Router,
    private auth: AppAuthService,
    public shareservice:ShareServiceService) { }

  ngOnInit(): void {
  }
  onSubmit() {
    alert('Form Submitted succesfully!!!\n Check the values in browser console.');
    console.table(this.userModal);
  }
  sendNotification() {
    debugger
    //send notification...
    const modalRef = this.modalService.open(CustomModalComponent);
    modalRef.componentInstance.title = 'Notification Sent';
    modalRef.componentInstance.bodyHTML = '<p>Notification Succesfully sent.</p>';
    setTimeout(() => {
      modalRef.close();
     }, 4000);
  }
}

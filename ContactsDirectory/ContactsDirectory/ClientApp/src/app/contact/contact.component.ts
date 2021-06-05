import { Component, OnInit, Inject } from '@angular/core';
import { Contact } from '../model/Contact.model';
import { HttpClient } from '@angular/common/http';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.css']
})
export class ContactComponent implements OnInit {

  public contact: Contact;
  public qpContactId: number;

  constructor(http: HttpClient,
    @Inject('BASE_URL') baseUrl: string,
    public router: Router,
    public route: ActivatedRoute,
  )
  {

      this.route.queryParams.subscribe(params => {
        this.qpContactId = params['ContactId'];
      });

    http.get<Contact>(baseUrl + 'api/Contacts/' + this.qpContactId).subscribe(result => {
        this.contact = result;
    }, error => console.error(error));
  }

  backToTaskList()
  {
    this.router.navigate(['/'], {
      skipLocationChange: true
    });
  }

  ngOnInit() {
  }

}

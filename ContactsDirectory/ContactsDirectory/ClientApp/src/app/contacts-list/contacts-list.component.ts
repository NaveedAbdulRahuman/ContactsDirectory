import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Contact } from '../model/Contact.model';

@Component({
  selector: 'app-contacts-list',
  templateUrl: './contacts-list.component.html',
  styleUrls: ['./contacts-list.component.css']
})
export class ContactsListComponent implements OnInit {

  public contacts: Contact[];
  public filterText: string;

  constructor(public http: HttpClient,
    @Inject('BASE_URL') public baseUrl: string,
    public router: Router
  )
  {
    http.get<Contact[]>(baseUrl + 'api/Contacts/').subscribe(result => {
      this.contacts = result;
    }, error => console.error(error));
  }

  viewContact(ContactId) {
    this.router.navigate(['contact'], {
      queryParams: {
        ContactId: ContactId,
      },
      skipLocationChange: true,
    });
  }

  searchContacts(event: any) {
    this.filterText = event.target.value;
    this.http.get<Contact[]>(
      this.baseUrl + 'api/Contacts/GetContacts/?nameLike=' + this.filterText
    ).subscribe(result =>
    {
      this.contacts = result;
    }, error => console.error(error));
  }

  ngOnInit() {
  }

}

import { Component, OnInit } from '@angular/core';
import { CommonService } from './../../shared/common.service';
import { Book } from '../../domain/book';
import { Subject } from 'rxjs/Subject';

@Component({
  selector: 'app-list-books',
  templateUrl: './list-books.component.html',
  styleUrls: ['./list-books.component.css']
})
export class ListBooksComponent implements OnInit {

  books: Book[] = [];
  dtOptions: DataTables.Settings = {};
  dtTrigger: Subject<any> = new Subject();

  constructor(private service: CommonService) { }


  ngOnInit() {

    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 5,
      lengthChange: false,
      language: {
        emptyTable: "No records available ",
        infoEmpty: "No records available ",
        zeroRecords:"No records available"
      }
    };

    this.service.getBooks().subscribe(
      (result: any) => {
        this.books = result.data;
        this.dtTrigger.next();
      },
      (error: any) => {
        alert(error.message);
      });
  }
}

import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommonService } from './../../shared/common.service';
import { Book } from '../../domain/book';
import { NgForm } from '@angular/forms';


@Component({
  selector: 'app-book-detail',
  templateUrl: './book-detail.component.html',
  styleUrls: ['./book-detail.component.css']
})

export class BookDetailComponent implements OnInit {
  cancelBook: any;
  id: number;
  book: Book;
  datevar: Date;
  isEditable: boolean = false;
  constructor(private route: ActivatedRoute, private service: CommonService) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.id = +params['id']; // (+) converts string 'id' to a number
      this.service.getBook(this.id).subscribe(
        (result: any) => {
          this.book = result.data;
          this.datevar = new Date(this.book.dateOfPublication);
        },
        (error: any) => {
          alert(error.message);
        });
    });
  }

  onSubmit() {
    this.cancelBook = this.book;
    this.service.putBook(this.book).subscribe(
      (result: any) => {
          this.isEditable = false;
      },
      (error: any) => {
        alert(error.message);
      });

  }

  onEdit() {
    this.isEditable = true;
    this.cancelBook = {
      id: this.book.id,
      name: this.book.name,
      authors: this.book.authors,
      numberOfPages: this.book.numberOfPages,
      dateOfPublication: this.book.dateOfPublication
    };
  }

  onCancel() {
    this.isEditable = false;
    this.book = this.cancelBook;
  }
}

import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { AppRoutingModule } from './app-routing.module';
import { DataTablesModule } from 'angular-datatables';
import 'rxjs/add/operator/toPromise';
import { HttpClient } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { CommonService } from './shared/common.service';
import { AppComponent } from './app.component';
import { ListBooksComponent } from './Books/list-books/list-books.component';
import { BookDetailComponent } from './Books/book-detail/book-detail.component'


@NgModule({
  declarations: [
    AppComponent,
    ListBooksComponent,
    BookDetailComponent
  ],
  imports: [
    BrowserModule,
    DataTablesModule,
    AppRoutingModule,
    FormsModule,
    HttpModule,
    HttpClientModule,
    ReactiveFormsModule,
  ],
  providers: [
    CommonService,
    HttpClient
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';
import { catchError, map, tap } from 'rxjs/operators';
import { GlobalConstants } from './../app.global';
import { identifierModuleUrl } from '@angular/compiler';
import { debug } from 'util';
import { Book } from '../domain/book';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class CommonService {

  constructor(private http: HttpClient) { }

  getBooks(): Observable<any> {
    let url: string = GlobalConstants.API_BASE_URL + 'Books';
    return this.http.get(url);
  }

  getBook(id: number): Observable<any> {
    let url: string = GlobalConstants.API_BASE_URL + 'Books/' + id;
    return this.http.get(url);
  }

  putBook(book: Book): Observable<any> {
    let url: string = GlobalConstants.API_BASE_URL + 'Books/';
    return this.http.put(url, book);
  }
}


import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListBooksComponent } from './Books/list-books/list-books.component';
import { BookDetailComponent } from './Books/book-detail/book-detail.component'

const routes: Routes = [
    {
        path: '',
        component: ListBooksComponent,
    },
    {
        path: 'book-detail/:id', 
        component: BookDetailComponent
    }
];

@NgModule({
    imports: [
        RouterModule.forRoot(routes)
    ],
    exports: [
        RouterModule
    ],
    declarations: []
})
export class AppRoutingModule { }
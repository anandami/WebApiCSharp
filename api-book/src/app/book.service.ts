import { Injectable } from '@angular/core';
import { Book } from './book';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HttpHeaders } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class BookService {

  private url = environment.host

  private book:Book;

  constructor(private http:HttpClient) { }
  
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type':  'application/json',
      'Authorization': 'my-auth-token'
    })
  };

  public save(book:Book):Observable<Book>{
    return this.http.post<Book>(this.url + "insert/", book,this.httpOptions);
    
  }

  public listBook():Observable<Book[]>{
    return this.http.get<Book[]>(this.url + "getalldata");
  }

  public delete(id:number):Observable<any>{
    return this.http.delete(this.url + "delete/" + id);
  }

  public search(id:number):Observable<Book[]>{
    return this.http.get<Book[]>(this.url + "search/" + id);
    
  }
  public update(id:number,book:Book):Observable<Book[]>{
    return this.http.put<Book[]>(this.url + "update/" + id, book,this.httpOptions);
    
  }
  public getBook(id):Observable<Book[]>{
    return this.http.get<Book[]>(`${this.url}/${id}`);
  }
}

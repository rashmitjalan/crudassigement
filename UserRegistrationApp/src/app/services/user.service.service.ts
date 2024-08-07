import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../models/user.model';
import { catchError, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserServiceService {
  private apiUrl = 'http://localhost:5141/api/users';

  constructor(private http: HttpClient) { }

  getUsers(pageNumber: number, pageSize: number): Observable<User[]> {
    return this.http.get<User[]>(`${this.apiUrl}?pageNumber=${pageNumber}&pageSize=${pageSize}`);
  }

  getUser(id: number): Observable<User> {
    return this.http.get<User>(`${this.apiUrl}/${id}`);
  }

  createUser(user: User): Observable<User> {
    const formData = new FormData();
    Object.keys(user).forEach(key => {
      const value = (user as any)[key];
      if (value !== undefined && value !== null) {
        formData.append(key, value.toString());
      }
    });
    if (user.profileImage) {
      formData.append('profileImage', user.profileImage);
    }
    return this.http.post<User>(this.apiUrl, formData).pipe(
      catchError(error => {
        // Log or handle error
        return throwError(() => error);
      })
    );
  }

  updateUser(id: number, user: User): Observable<void> {
    const formData = new FormData();
    Object.keys(user).forEach(key => {
      const value = (user as any)[key];
      if (value !== undefined && value !== null) {
        formData.append(key, value.toString());
      }
    });
    if (user.profileImage) {
      formData.append('profileImage', user.profileImage);
    }
    return this.http.put<void>(`${this.apiUrl}/${id}`, formData).pipe(
      catchError(error => {
        // Log or handle error
        return throwError(() => error);
      })
    );
  }

  deleteUser(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}

import { Component, OnInit } from '@angular/core';
import { UserServiceService } from '../services/user.service.service';
import { User } from '../models/user.model';
import { NgForm } from '@angular/forms';
import { catchError } from 'rxjs';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  users: User[] = [];
  userForm: User = {
    fullName: '',
    profileImage: undefined,
    gender: '',
    address: '',
    city: '',
    pin: 0,
    state: '',
    country: '',
    email: '',
    contact: '',
    educationQualification: '',
    designation: ''
  };
  countries: string[] = ['United States', 'Canada', 'United Kingdom', 'Australia', 'India', 'Germany', 'France', 'Italy', 'Spain', 'Brazil'];
  pageNumber = 1;
  pageSize = 8;
  errorMessage: string = '';

  constructor(
    private userService: UserServiceService,
    private toastr: ToastrService // Inject ToastrService
  ) { }

  ngOnInit(): void {
    this.getUsers();
  }

  getUsers(): void {
    this.userService.getUsers(this.pageNumber, this.pageSize).subscribe(users => this.users = users);
  }

  createUser(): void {
    this.userService.createUser(this.userForm).pipe(
      catchError(error => {
        this.errorMessage = this.getErrorMessage(error);
        this.toastr.error(this.errorMessage, 'Error'); // Show error notification
        return [];
      })
    ).subscribe(() => {
      this.getUsers();
      this.resetForm();
      this.toastr.success('User created successfully!', 'Success'); // Show success notification
    });
  }

  updateUser(id: number | undefined): void {
    if (id === undefined) {
      console.error('User ID is undefined');
      return;
    }
    this.userService.updateUser(id, this.userForm).pipe(
      catchError(error => {
        this.errorMessage = this.getErrorMessage(error);
        this.toastr.error(this.errorMessage, 'Error'); // Show error notification
        return [];
      })
    ).subscribe(() => {
      this.getUsers();
      this.resetForm();
      this.toastr.success('User updated successfully!', 'Success'); // Show success notification
    });
  }

  deleteUser(id: number | undefined): void {
    if (id === undefined) {
      console.error('User ID is undefined');
      return;
    }
    this.userService.deleteUser(id).subscribe(() => {
      this.getUsers();
      this.toastr.success('User deleted successfully!', 'Success'); // Show success notification
    });
  }

  setUser(user: User): void {
    this.userForm = { ...user };
  }

  resetForm(): void {
    this.userForm = {
      fullName: '',
      profileImage: undefined,
      gender: '',
      address: '',
      city: '',
      pin: 0,
      state: '',
      country: '',
      email: '',
      contact: '',
      educationQualification: '',
      designation: ''
    };
    this.errorMessage = '';
    // Clear the file input field
  const fileInput = document.querySelector('input[type="file"]') as HTMLInputElement;
  if (fileInput) {
    fileInput.value = ''; // This clears the file input field
  }
  }

  onFileSelected(event: any): void {
    const file: File = event.target.files[0];
    if (file) {
      this.userForm.profileImage = file;
    }
  }

  private getErrorMessage(error: any): string {
    if (error.error && error.error.errors) {
      const errors = error.error.errors;
      return Object.values(errors).join(' ');
    }
    return 'An unexpected error occurred.';
  }
}

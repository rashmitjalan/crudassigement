export interface User {
  id?: number;
  fullName: string;
  profileImage?: File; // Change to File for image upload
  gender: string;
  address: string;
  city: string;
  pin: number;
  state: string;
  country: string;
  email: string;
  contact: string;
  educationQualification: string;
  designation: string;
}
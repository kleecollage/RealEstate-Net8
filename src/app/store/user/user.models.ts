import { User } from '@app/models/backend';

// LOGIN RESPONSE
export { User as UserResponse } from '@app/models/backend/user';

// LOGIN REQUEST
export interface EmailPasswordCredentials {
  email: string;
  password: string;
}

// REGISTER REQUEST
export interface UserRequest extends User {
  password: string;
}

export type UserCreateRequest = Omit<UserRequest, 'token'>

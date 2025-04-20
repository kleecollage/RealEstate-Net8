import { Action } from '@ngrx/store';
import { EstateCreateRequest, EstateResponse } from './save.models';

export enum Types {
  CREATE = '[Estate] Create: Start',
  CREATE_SUCCESS = '[Estate] Create: Success',
  CREATE_ERROR = '[Estate] Create: Error',
}

export class Create implements Action {
  readonly type = Types.CREATE;
  constructor(public estate: EstateCreateRequest) {}
}

export class CreateSuccess implements Action {
  readonly type = Types.CREATE_SUCCESS;
  constructor(public estate: EstateResponse) {}
}

export class CreateError implements Action {
  readonly type = Types.CREATE_ERROR;
  constructor(public error: string) {}
}

export type All = Create | CreateSuccess | CreateError;

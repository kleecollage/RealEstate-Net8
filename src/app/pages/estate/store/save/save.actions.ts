import { Action } from '@ngrx/store';
import { EstateCreateRequest, EstateResponse } from './save.models';

export enum Types {
  CREATE = '[Estate] Create: Start',
  CREATE_SUCCESS = '[Estate] Create: Success',
  CREATE_ERROR = '[Estate] Create: Error',

  READ = '[Estate] Read: Start',
  READ_SUCCESS = '[Estate] Read: Success',
  READ_ERROR = '[Estate] Read: Error',
}

// ==============================   CREATE   ============================== //
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
// ==============================   READ   ============================== //
export class Read implements Action {
  readonly type = Types.READ;
  constructor() {}
}

export class ReadSuccess implements Action {
  readonly type = Types.READ_SUCCESS;
  constructor(public estate: EstateResponse[]) {}
}

export class ReadError implements Action {
  readonly type = Types.READ_ERROR;
  constructor(public error: string) {}
}

export type All =
  Create | CreateSuccess | CreateError |
  Read | ReadSuccess | ReadError;

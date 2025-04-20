import * as fromActions from './save.actions';
import { EstateResponse } from './save.models';

export interface ListState {
  estates: EstateResponse[] | null,
  estate: EstateResponse | null,
  loading: boolean | null;
  error: string | null;
}

export const initialState: ListState = {
  estates: null,
  estate: null,
  loading: null,
  error: null,
}

export function reducer(state: ListState = initialState, action: fromActions.All | any) {
  switch(action.type) {
    case fromActions.Types.CREATE: {
      return { ...state, loading: true, error: null };
    }

    case fromActions.Types.CREATE_SUCCESS: {
      return { ...state, loading: false, error: null, estate: action.estate };
    }

    case fromActions.Types.CREATE_ERROR: {
      return { ...state, loading: false, error: action.error, estate: null };
    }

    case fromActions.Types.READ: {
      return { ...state, loading: true };
    }

    case fromActions.Types.READ_SUCCESS: {
      return { ...state, loading: false, estates: action.estates };
    }

    case fromActions.Types.READ_ERROR: {
      return { ...state, loading: false, error: action.error, estates: null };
    }

    default: {
      return state;
    }
  }
}

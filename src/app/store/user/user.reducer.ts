import { UserResponse } from "@app/store/user/user.models";
import * as fromActions from "./user.actions";

export interface UserState {
  entity: UserResponse | null;
  id: string | null;
  loading: boolean | null;
  error: string | null;
}

const initialState: UserState = {
  entity: null,
  id: null,
  loading: null,
  error: null,
};

export function reducer(state = initialState, action: fromActions.All | any): UserState {
  switch (action.type) {
    // ==============================   init   ============================== //
    case fromActions.Types.INIT: {
      return { ...state, loading: true };
    }

    case fromActions.Types.INIT_AUTHORIZED: {
      return { ...state, loading: false, entity: action.user, id: action.id, error: null };
    }

    case fromActions.Types.INIT_UNAUTHORIZED: {
      return { ...state, loading: false, entity: null, id: null, error: null };
    }

    case fromActions.Types.INIT_ERROR: {
      return { ...state, loading: false, entity: null, id: null, error: action.error };
    }
    // ==============================   login   ============================== //
    case fromActions.Types.SIGN_IN_EMAIL: {
      return { ...state, loading: true, entity: null, id: null, error: null };
    }

    case fromActions.Types.SIGN_IN_EMAIL_SUCCESS: {
      return { ...state, loading: false, entity: action.user, id: action.id, error: null };
    }

    case fromActions.Types.SIGN_IN_EMAIL_ERROR: {
      return { ...state, loading: false, entity: null, id: null, error: action.error };
    }
    // ==============================   register   ============================== //
    case fromActions.Types.SIGN_UP_EMAIL: {
      return { ...state, loading: true, entity: null, id: null, error: null };
    }

    case fromActions.Types.SIGN_UP_EMAIL_SUCCESS: {
      return { ...state, loading: false, entity: action.user, id: action.id, error: null };
    }

    case fromActions.Types.SIGN_UP_EMAIL_ERROR: {
      return { ...state, loading: false, entity: null, id: null, error: action.error };
    }
    // ==============================   logout   ============================== //
    case fromActions.Types.SIGN_OUT_EMAIL: {
      return { ...initialState };
    }

    case fromActions.Types.SIGN_OUT_EMAIL_SUCCESS: {
      return { ...initialState };
    }

    case fromActions.Types.SIGN_OUT_EMAIL_ERROR: {
      return { ...state, loading: false, entity: null, id: null, error: action.error };
    }

    default: {
      return state;
    }
  }
}

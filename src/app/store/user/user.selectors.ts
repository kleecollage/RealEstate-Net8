import { createSelector, createFeatureSelector } from '@ngrx/store';
import { UserState } from '@app/store/user/user.reducer';

export const getUserState = createFeatureSelector<UserState>('user');

export const getUser = createSelector(getUserState, (state) => state.entity);

export const getLoading = createSelector(getUserState, (state) => state.loading);

export const getIsAuthorized = createSelector(getUserState, (state) => !!state.entity?.email);

import { createSelector } from '@ngrx/store';
import { EstateStatus, getEstateStatus } from '../index';
import { ListState } from './save.reducer';

export const getListState = createSelector(getEstateStatus, (state: EstateStatus) => state.list);

export const getLoading = createSelector(getListState, (state: ListState) => state.loading);

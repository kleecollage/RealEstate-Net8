import { ActionReducerMap, createFeatureSelector } from '@ngrx/store';
import { SaveEffects } from './save/save.effects';
import * as fromList from './save/save.reducer';

export interface EstateStatus {
 list: fromList.ListState
}

export const reducers: ActionReducerMap<EstateStatus> = {
  list: fromList.reducer
}

export const effects: any = [
  SaveEffects
]

export const getEstateStatus = createFeatureSelector<EstateStatus>('estate');

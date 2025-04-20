import { Estate } from '@app/models/backend/estate';
export { Estate as EstateResponse } from '@app/models/backend/estate';

export type EstateCreateRequest = Omit<Estate, 'id' | 'createdAt'>;


import clone from 'clone';
import {default as fastCompare} from 'react-fast-compare';

export const deepClone = <TType>(obj: TType): TType => {
  return clone(obj);
};

export const isEqual = <P, N>(prev: P, next: N): boolean => {
  return fastCompare(prev, next);
};

export const isNullOrUndefined = (value: number): boolean => {
  return (value === null || value === undefined);
};

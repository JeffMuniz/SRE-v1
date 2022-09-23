
import styled, {css} from 'styled-components/macro';
import {BaseLabel} from '~/styled';

export const Wrapper = styled.div`
  align-self: center;
  display: flex;
  min-width: 500px;

`;

export const StepWrapper = styled.div`
  align-self: flex-end;
  display: flex;
  flex-direction: column;
  justify-content: flex-end;
  width: 16px;
`;

export const Label = styled(BaseLabel)`
  align-self: center;
  font-size: 14px;
  font-style: italic;
  margin-bottom: 10px;
  min-width: 110px;
  text-align: center;
`;

export const Circle = styled.div<{ highlight: boolean; }>`
  align-self: center;
  border: solid 4px;
  border-radius: 50%;
  height: 16px;
  transition: ease .3s;
  width: 16px;

  ${({theme: {colors: {palette}}, highlight}) => css`
    border-color: ${highlight ? palette.green.main : palette.lightGray.main};
  `}
`;

export const Line = styled.div`
  align-self: flex-end;
  height: 2px;
  margin-bottom: 6px;
  width: 110px;

  ${({theme: {colors}}) => css`
    background-color: ${colors.palette.lightGray.main};
  `}
`;


import styled, {css} from 'styled-components';
import {BaseLabel} from '~/styled';

export const Wrapper = styled.div`
  display: flex;
  flex-direction: column;
  width: 100%;
`;

export const Label = styled(BaseLabel)`
  align-self: flex-end;
  font-size: 12px;
  font-style: italic;
  margin-bottom: 15px;
`;

export const Indicator = styled.div`
  display: flex;
  height: 4px;
  width: 100%;
`;

export const Completed = styled.div<{
  proportion: number;
}>`
  align-self: center;
  height: 100%;
  transition: ease .6s;
  width: 1px;
  
  ${({proportion, theme: {colors}}) => css`
    background-color: ${colors.feedback.success};
    flex-grow: ${proportion};
  `}
`;

export const Pending = styled(Completed)`
  ${({proportion, theme: {colors}}) => css`
    background-color: ${colors.feedback.neutral};
    flex-grow: ${proportion};
  `}
`;


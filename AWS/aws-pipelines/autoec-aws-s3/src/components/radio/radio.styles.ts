
import styled, {css} from 'styled-components';

import {BaseLabel, pointer} from '~/styled';

export const Wrapper = styled.div`
  ${pointer()}
  display: flex;
`;

export const HiddenRadio = styled.input.attrs({
  type: 'radio',
})`
  opacity: 0;
  height: 20px;
  width: 20px;
  align-self: center;
  margin-right: -20px;
  position: relative;
  z-index: 999;

  &:checked ~ div > div {
    transform: scale(1);
  }
`;

export const Circle = styled.div`
  align-self: center;
  border-radius: 10px;
  display: flex;
  height: 20px;
  justify-content: center;
  margin-right: 15px;
  width: 20px;
  
  ${({theme: {colors}}) => css`
    border: solid 2px ${colors.palette.lightGray.main};
  `}
`;

export const InnerCircle = styled(Circle)`
  border: none;
  height: 10px;
  margin: 0;
  transform: scale(0);
  transition: ease .3s;
  width: 10px;

  ${({theme: {colors}}) => css`
    background-color: ${colors.feedback.success};
  `}
`;

export const Label = styled(BaseLabel)`
  align-self: center;
  font-size: 20px;
`;

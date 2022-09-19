
import styled, {css} from 'styled-components';
import {BaseLabel, pointer} from '~/styled';

const {PUBLIC_URL} = process.env;

export const Wrapper = styled.div`
  ${pointer()}
  display: flex;
`;

export const HiddenCheckbox = styled.input.attrs({
  type: 'checkbox',
})`
  opacity: 0;
  height: 20px;
  width: 20px;
  align-self: flex-start;
  margin-right: -20px;
  position: relative;
  z-index: 999;

  &:checked ~ div {
    border-width: 0px;
    & > div {
      transform: scale(1);
    }
  }
`;

export const Square = styled.div`
  align-self: flex-start;
  border-radius: 3px;
  display: flex;
  height: 20px;
  justify-content: center;
  margin-right: 15px;
  transition: ease .1s;
  width: 20px;
  
  ${({theme: {colors}}) => css`
    border: solid 2px ${colors.palette.lightGray.main};
  `}
`;

export const InnerSquare = styled(Square)`
  border: none;
  color: white;
  margin: 0;
  transform: scale(0);
  transition: ease .3s;

  ${({theme: {colors}}) => css`
    background-color: ${colors.feedback.success};
  `}
`;

export const CheckMark = styled.img.attrs({
  src: `${PUBLIC_URL}/icons/check-mark.svg`,
})`
  align-self: center;
  height: 10px;
`;

export const Label = styled(BaseLabel)`
  align-self: flex-start;
  flex-grow: 1;
  font-size: 20px;
  width: 1px;
`;

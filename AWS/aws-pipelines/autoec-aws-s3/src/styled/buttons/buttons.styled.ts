
import styled, {css} from 'styled-components';
import {pointer} from '../helpers/helpers.styled';
import {mqTablet} from '../mq/mq.styled';

export const PrimaryButton = styled.button`
  ${pointer()}
  border: none;
  border-radius: 5px;
  font-size: 16px;
  font-weight: 700;
  height: 48px;
  line-height: 10px;
  min-width: 150px;
  opacity: 1;
  padding: 0 15px;
  text-transform: uppercase;
  transition: ease .3s;
  width: 100%;

  ${({theme: {colors}}) => css`
    background-color: ${colors.palette.pink.main};
    color: ${colors.palette.pink.contrast};

    &:hover {
      background-color: ${colors.palette.pink.dark};
    }
  `}

  ${({disabled, theme: {colors}}) => disabled && css`
    &, &:hover {
      background-color: ${colors.palette.lightGray.main};
    }
  `}

  ${mqTablet(css`
    width: 300px;
  `)}
`;

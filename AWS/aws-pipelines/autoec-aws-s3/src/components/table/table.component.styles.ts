
import styled, {css} from 'styled-components';
import {mqTablet} from '~/styled';

export const Wrapper = styled.div`
  border-radius: 5px;
  overflow: hidden;

  ${({theme: {colors}}) => css`
    border: 1px solid ${colors.palette.lightGray.main};
  `}
`;

export const Row = styled.div`
  align-self: flex-start;
  display: flex;

  ${({theme: {colors}}) => css`
    border-bottom: 1px solid ${colors.palette.lightGray.main};

    &:last-child {
      border: none;
    }
  `}

  ${mqTablet(css`
    &:first-child > div {
      height: 80px;
    }
  `)}
`;

export const Item = styled.div<{ highlight: boolean; }>`
  align-self: flex-start;
  display: flex;
  flex-grow: 1;
  height: 85px;
  justify-content: center;
  padding: 0 10px;
  text-align: center;
  width: 1px;
  
  ${({theme: {colors}}) => css`
    border-right: 1px solid ${colors.palette.lightGray.main};
  `}

  &:last-child {
    border: none;
  }

  ${({highlight, theme: {colors}}) => highlight && css`
    background-color: #F8F8F8;
  `}

  ${mqTablet(css`
    height: 50px;
    justify-content: flex-start;
    text-align: left;
  `)}
`;

export const Content = styled.div`
  align-self: center;
`;

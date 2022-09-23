
import {PulseLoader} from 'react-spinners';
import styled, {css} from 'styled-components';
import {dim} from '~/styled/helpers/helpers.styled';

export const Wrapper = styled.div<{ show: boolean; }>`
  ${dim()}
  align-items: center;
  display: flex;
  height: 100vh;
  justify-content: center;
  left: 0;
  opacity: 1;
  padding: 20px;
  position: fixed;
  top: 0;
  transition: ease .6s;
  width: 100vw;
  z-index: 999;

  ${({show}) => !show && css`
    opacity: 0;
    z-index: -1;
  `}
`;

export const Loading = styled(PulseLoader).attrs(({theme: {colors}}) => ({
  color: colors.palette.pink.main,
}))``;

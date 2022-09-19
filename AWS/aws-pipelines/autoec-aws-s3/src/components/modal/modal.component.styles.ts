
import styled, {css} from 'styled-components';
import {
 dim, noScrollbar, scrollingShadow,
} from '~/styled/helpers/helpers.styled';

export const Wrapper = styled.div<{ show: boolean; }>`
  ${dim()}
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

export const Content = styled.div`
  ${noScrollbar()}
  ${scrollingShadow()}
  align-self: center;
  border-radius: 10px;
  display: flex;
  flex-direction: column;
  max-height: 100%;
  max-width: 500px;
  overflow-y: auto;
  padding: 30px 20px;
  transition: ease .6s;
  width: calc(100% - 40px);
  
  ${({theme: {colors}}) => css`
    background-color: ${colors.palette.white.main};
  `}
`;

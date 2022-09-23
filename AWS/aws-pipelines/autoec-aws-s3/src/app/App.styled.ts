
import styled, {css} from 'styled-components/macro';
import {mqTablet} from '~/styled';

export const Wrapper = styled.div`
  display: flex;
  flex-direction: column;
  min-height: 100vh;
`;

export const PageWrapper = styled.div`
  display: flex;
  flex-direction: column;
  flex-grow: 1;
  padding: 30px 20px;
  padding-top: 80px;

  ${mqTablet(css`
    align-self: center;
    max-width: 900px;
    padding: 110px 50px 30px;
    width: 100%;
  `)}
`;

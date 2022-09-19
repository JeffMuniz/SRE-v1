
import styled, {css} from 'styled-components';
import {
 BaseLabel, PageSubtitle, mqTablet,
} from '~/styled';

export const Wrapper = styled.div`
  display: flex;
  flex-direction: column;
  flex-grow: 1;
`;

export const ICPageSubtitle = styled(PageSubtitle)`
  display: none;

  ${mqTablet(css`
    display: block;
  `)}
`;

export const Info = styled(BaseLabel)`
  display: none;

  ${mqTablet(css`
    display: block;
    margin-bottom: 30px;
  `)}
`;

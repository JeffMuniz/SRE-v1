
import styled, {css} from 'styled-components';

import {
 PageSubtitle, PageTitle, mqTablet,
} from '~/styled';

export const Wrapper = styled.div`
  display: flex;
  flex-direction: column;
  flex-grow: 1;
  margin-top: -15px;
`;

export const HPageTitle = styled(PageTitle)`
  margin-bottom: 15px;

  ${mqTablet(css`
    margin-bottom: 70px;
  `)}
`;

export const HPageSubtitle = styled(PageSubtitle)`
  margin-bottom: 30px;

  ${mqTablet(css`
    display: none;
  `)}
`;

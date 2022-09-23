
import styled, {css} from 'styled-components';
import {
 BaseLabel, PageSubtitle, mqTablet,
} from '~/styled';

export const Wrapper = styled.div`
  display: flex;
  flex-direction: column;
  flex-grow: 1;
`;

export const NCPageSubtitle = styled(PageSubtitle)`
  margin-bottom: 5px;
`;

export const Info = styled(BaseLabel)`
  margin-bottom: 20px;
  ${mqTablet(css`
    margin-bottom: 30px;
  `)}
`;

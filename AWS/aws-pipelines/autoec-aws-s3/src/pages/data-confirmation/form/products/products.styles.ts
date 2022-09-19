
import styled, {css} from 'styled-components';
import {Clickable, mqTablet} from '~/styled';

export const Wrapper = styled.div`
  margin-bottom: 30px;
`;

export const EditLabel = styled(Clickable)``;

export const TabletInlineWrapper = styled.div`
  margin-top: 20px;
  ${mqTablet(css`
    display: flex;
    flex-wrap: wrap; 
  `)}
`;


import styled, {css} from 'styled-components';
import {
 PageSubtitle, PrimaryButton, Spacer, mqTablet,
} from '~/styled';

export const Wrapper = styled.div`
  display: flex;
  flex-direction: column;
  flex-grow: 1;
`;

export const FATPageSubtitle = styled(PageSubtitle)`
  margin-bottom: 20px;
`;

export const FATSpacer = styled(Spacer)``;

export const ContinueButton = styled(PrimaryButton)`
  ${mqTablet(css`
    align-self: flex-end;
  `)}
`;

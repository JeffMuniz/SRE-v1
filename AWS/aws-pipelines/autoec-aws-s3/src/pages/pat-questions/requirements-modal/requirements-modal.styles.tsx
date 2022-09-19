
import styled, {css} from 'styled-components';
import {Modal} from '~/components';
import {
 BaseLabel, PageSubtitle, PrimaryButton, mqTablet,
} from '~/styled';

export const Wrapper = styled(Modal)``;

export const RMPageSubtitle = styled(PageSubtitle)`
  margin-bottom: 20px;
`;

export const Label = styled(BaseLabel)`
  margin-bottom: 30px;
`;

export const TryAgainButton = styled(PrimaryButton)`
  ${mqTablet(css`
    align-self: center;
  `)}
`;

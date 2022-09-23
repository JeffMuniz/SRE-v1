import styled, {css} from 'styled-components';
import {
CheckedLabel, ErrorModal, LoadingButton,
} from '~/components';
import {PageSubtitle, mqTablet} from '~/styled';

export const Wrapper = styled.div`
  display: flex;
  flex-direction: column;
`;

export const DCPageSubtitle = styled(PageSubtitle)`
  margin-bottom: 20px;
`;

export const DCCheckedLabel = styled(CheckedLabel)`
  margin-bottom: 20px;
`;

export const FinishButton = styled(LoadingButton)`
  margin-top: 130px;

  ${mqTablet(css`
    align-self: flex-end;
  `)}
`;

export const FErrorModal = styled(ErrorModal)``;

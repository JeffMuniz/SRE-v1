
import styled, {css} from 'styled-components';
import {
CheckedLabel, ErrorModal, LoadingButton,
} from '~/components';
import {
PageSubtitle, Spacer, mqTablet,
} from '~/styled';

export const Wrapper = styled.div`
  display: flex;
  flex-direction: column;
  flex-grow: 1;
`;

export const Form = styled.form`
  display: flex;
  flex-direction: column;
  flex-grow: 1;
`;

export const FSpacer = styled(Spacer)``;

export const SubmitButton = styled(LoadingButton).attrs({
  type: 'submit',
})`
  ${mqTablet(css`
    align-self: flex-end;
  `)}
`;

export const DCPageSubtitle = styled(PageSubtitle)`
  margin-bottom: 20px;
`;

export const DCCheckedLabel = styled(CheckedLabel)`
  margin-bottom: 20px;
`;

export const FinishButton = styled(LoadingButton).attrs({
  type: 'submit',
})`
  margin-top: 130px;

  ${mqTablet(css`
    align-self: flex-end;
  `)}
`;

export const FErrorModal = styled(ErrorModal)``;


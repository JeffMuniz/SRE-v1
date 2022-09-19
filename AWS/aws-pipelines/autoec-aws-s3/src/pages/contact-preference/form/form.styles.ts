
import styled, {css} from 'styled-components';
import {
ErrorModal, LoadingButton, Radio,
} from '~/components';
import {Spacer, mqTablet} from '~/styled';

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

export const PreferredMediumInput = styled(Radio)`
  margin-bottom: 15px;
`;

export const FSpacer = styled(Spacer)``;

export const SubmitButton = styled(LoadingButton).attrs({
  type: 'submit',
})`
  ${mqTablet(css`
    align-self: flex-end;
  `)}
`;

export const FErrorModal = styled(ErrorModal)``;

import styled, {css} from 'styled-components';
import {
DigitInput, ErrorModal, LoadingButton,
} from '~/components';
import {
 BaseLabel, Clickable, Spacer, mqTablet,
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

export const FDigitInput = styled(DigitInput).attrs({
  digits: 6,
  label: 'Código:',
})`
  margin-bottom: 10px;
  label {
    display: none;
  }

  ${mqTablet(css`
    label {
      display: block;
    }
  `)}
`;

export const ResendLabel = styled(Clickable)`
  font-size: 12px;

  ${mqTablet(css`
    font-size: 14px;
  `)}
`;

export const FSpacer = styled(Spacer)``;

export const SubmitButton = styled(LoadingButton).attrs({
  type: 'submit',
})`
  align-self: flex-end;
`;

export const InvalidCodeLabel = styled(BaseLabel)`
  font-size: 12px;
  margin-bottom: 8px;
  ${({theme: {colors}}) => css`
    color: ${colors.feedback.failure};
  `}
`;

export const FErrorModal = styled(ErrorModal)``;

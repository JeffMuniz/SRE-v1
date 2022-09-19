
import styled, {css} from 'styled-components';
import {ErrorModal, Radio} from '~/components';
import {
 BaseLabel, Clickable, PrimaryButton, Spacer, mqTablet,
} from '~/styled';

export const Wrapper = styled.div`
  display: flex;
  flex-direction: column;
  flex-grow: 1;
`;

export const FormEl = styled.form`
  display: flex;
  flex-direction: column;
  flex-grow: 1;
`;

export const NameRadioInput = styled(Radio).attrs({
  name: 'name',
})`
  margin-bottom: 15px;
`;

export const FallbackInfo = styled(BaseLabel)`
  align-self: center;
  font-size: 12px;
  width: 190px;

  ${mqTablet(css`
    align-self: flex-start;
    width: unset;
  `)}
`;

export const Link = styled(FallbackInfo).attrs({
  as: Clickable,
})`
  font-weight: bold;
`;

export const FSpacer = styled(Spacer)``;

export const SubmitButton = styled(PrimaryButton).attrs({
  type: 'submit',
})`
  ${mqTablet(css`
    align-self: flex-end;
  `)}
`;

export const FErrorModal = styled(ErrorModal)``;

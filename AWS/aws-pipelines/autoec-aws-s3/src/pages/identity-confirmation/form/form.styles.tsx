import styled, {css} from 'styled-components';
import {
ErrorModal, LoadingButton, Radio,
} from '~/components';
import {
 BaseLabel, PageSubtitle, Spacer, mqTablet,
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

export const FPageSubtitle = styled(PageSubtitle)`
  margin-bottom: 20px;

  ${mqTablet(css`
    display: none;
  `)}
`;

export const Info = styled(BaseLabel)`
  display: none;

  ${mqTablet(css`
    display: block;
    margin-bottom: 10px;
  `)}
`;

export const InputWrapper = styled.div`
  display: flex;
  flex-direction: column;
  margin-bottom: 50px;

  ${mqTablet(css`
    margin-bottom: 20px;
  `)}
`;

export const MotherNameRadioInput = styled(Radio)`
  margin-bottom: 15px;

  ${mqTablet(css`
    margin-right: 30px;
    
    &:last-child {
      margin-right: 0;
    }
  `)}
`;

export const BirthdayInputWrapper = styled(InputWrapper)`
  ${mqTablet(css`
    flex-direction: row;
  `)}
`;

export const BirthdayRadioInput = styled(MotherNameRadioInput)``;

export const FSpacer = styled(Spacer)``;

export const SubmitButton = styled(LoadingButton).attrs({
  type: 'submit',
})`
  ${mqTablet(css`
    align-self: flex-end;
  `)}
`;

export const FErrorModal = styled(ErrorModal)``;

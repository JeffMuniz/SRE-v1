import {FC} from 'react';
import {
Button, ButtonText, Text, Title, Wrapper,
} from './error-modal.component.styles';

type Props = ErrorModal.Props;

const ErrorModal: FC<Props> = ({
  onCloseButtonClick,
  show,
  text,
  title,
  buttonText = 'Fechar',
}) => (
  <Wrapper
    className="error-modal"
    onBackgroundClick={onCloseButtonClick}
    show={show}
  >
    {!!title && <Title>{title}</Title>}
    <Text>{text}</Text>
    <Button onClick={onCloseButtonClick}>
      <ButtonText>
        {buttonText}
      </ButtonText>
    </Button>
  </Wrapper>
);

export default ErrorModal;

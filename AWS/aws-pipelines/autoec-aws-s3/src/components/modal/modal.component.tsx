
import React, {FC} from 'react';
import {useCallback} from 'react';
import {Content, Wrapper} from './modal.component.styles';

const Modal: FC<Modal.Props> = ({
  className,
  children,
  onBackgroundClick,
  show,
}) => {

  const handleContentWrapperClick = useCallback((event: React.MouseEvent) => {
    event.stopPropagation();
  }, [ ]);

  return (
    <Wrapper
      className={className}
      onClick={onBackgroundClick}
      show={show}
    >
      <Content onClick={handleContentWrapperClick}>
        {children}
      </Content>
    </Wrapper>
  );
};

export default Modal;

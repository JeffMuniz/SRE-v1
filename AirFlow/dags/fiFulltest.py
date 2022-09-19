from datetime import timedelta

from airflow import DAG
from airflow.operators.dummy import DummyOperator
from airflow.providers.microsoft.winrm.hooks.winrm import WinRMHook
from airflow.providers.microsoft.winrm.operators.winrm import WinRMOperator
from airflow.utils.dates import days_ago


[smtp]
# If you want airflow to send emails on retries,
#  failure, and you want to use
# the airflow.utils.email.send_email_smtp 
# function, you have to configure an
# smtp server here
smtp_host = smtp.machina.com
smtp_starttls = False
smtp_ssl = True
smtp_user = jmuniz1985@gmail.com                     
smtp_password =  some-crde-passin
smtp_port = 465
smtp_mail_from = jmuniz1985@gmail.com


default_args = {
    'owner': 'airflow',
    'email': ['jmuniz1985@gmail.com'],
    'email_on_failure': True,
    'email_on_retry': True,
    'email_on_success': True,
    'retry_exponential_backoff': True,
    'retry_delay' : timedelta(seconds=300),
    'retries': 3
}

with DAG(
    dag_id='FiFulltest',
    default_args=default_args,
    schedule_interval='0 0 * * *',
    start_date=days_ago(2),
    dagrun_timeout=timedelta(minutes=60),
    tags=['windows'],
) as dag:

    cmd = 'ls -l'
    result_mail = DummyOperator(task_id='result_mail')
    winRMHook = WinRMHook(ssh_conn_id='ssh_asturias_hml')

    t1 = WinRMOperator(
        task_id="fipreclose", command='echo fipreclose.bat',
        winrm_hook=winRMHook
        )
    t2 = WinRMOperator(
        task_id="mailfipreclose", command='echo ficlose',
        winrm_hook=winRMHook
        )
    t3 = WinRMOperator(
        task_id="ficlose",  command='echo ficlose',
        winrm_hook=winRMHook
        )
    t4 = WinRMOperator(
        task_id="mailficlose", command='echo ficlose',
        winrm_hook=winRMHook
        )
    t5 = WinRMOperator(
        task_id="fibigsend", command='echo ficlose',
        winrm_hook=winRMHook
        )
    t6 = WinRMOperator(
        task_id="mailfibigsend", command='echo ficlose',
        winrm_hook=winRMHook
        )

    [t1 >> t2 >> t3 >> t4 >> t5 >> t6] >> result_mail


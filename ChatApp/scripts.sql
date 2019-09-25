SET FOREIGN_KEY_CHECKS = 0;

DROP TABLE IF EXISTS `users`;

CREATE TABLE `users` (
  `username` varchar(55) UNIQUE,
  `password` varchar(500) DEFAULT NULL,

  PRIMARY KEY (`username`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;